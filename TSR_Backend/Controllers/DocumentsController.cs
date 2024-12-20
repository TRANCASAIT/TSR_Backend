using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TSR_Backend.Models;
using static TSR_Backend.Models.ServiceRequest;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.Document;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.SignalR;
using TSR_Backend.Hubs;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DocumentsController(ILogger<DocumentsController> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        #region adm-procedures
        [HttpPost("UpdateConsignmentNote")]
        [Authorize]
        public async Task<IActionResult> UpdateConsignmentNote(UpdateConsignmentNote _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateConsigmentNote(_obj, currentUser.Uid, currentUser.DynamicToken!);
                if (result.State == 1)
                {
                    return BadRequest(result);
                }
                else if (result.State == 401)
                {
                    return BadRequest(result);
                }
                else
                {
                    await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        


        [HttpPost("UploadDocumentAdmin")]
        [Authorize]
        public async Task<IActionResult> UploadDocumentAdmin([FromForm] UploadFile doc)
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                if (doc.DocumentId > 0 && doc.ServiceRequestId > 0 && doc.DocumentType > 0)
                {
                    if (doc.DocumentFile == null && doc.DocumentFile!.Length > 0)
                    {
                        result.Message = "The file is empty please check";
                        result.State = 1;
                        return BadRequest(result);
                    }
                    else
                    {
                        if (doc.DocumentType == 5 || doc.DocumentType == 8 || doc.DocumentType == 9)
                        {
                            //var filePath = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType) + "/" + doc.DocumentFile.FileName);

                            //var removeFile = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType));

                            //production
                            var filePath = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType) + "/" + doc.DocumentFile.FileName);
                            var removeFile = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType));

                            result = TSR_DAO.UploadDocumentAdmin(doc, currentUser.Uid, filePath, doc.DocumentFile.FileName, currentUser.DynamicToken!);

                            if (result.State == 0)
                            {
                                if (Directory.Exists(removeFile)) Directory.Delete(removeFile, true);

                                new FileInfo(filePath).Directory?.Create();
                                await using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    _logger.LogInformation($"Saving file [{doc.DocumentFile.FileName}]");
                                    await doc.DocumentFile.CopyToAsync(stream);
                                    _logger.LogInformation($"\t The uploaded file is saved as [{filePath}]");
                                }
                                await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                                await _hubContext.Clients.All.SendAsync("RecordUpdated");
                                return Ok(result);
                            }
                            else
                            {
                                if (result.Message!.Length > 0)
                                {
                                    if (result.Message == "El layout no ha sido aceptado o el TMW no ha sido agregado o el num. cp no se ha agregado o esta solicitud ha sido completada" || result.Message == "File already uploaded" || result.Message == "The TMW must be added" || result.Message == "The layout has not been uploaded yet" || result.Message == "The consignment note has not been added yet" || result.Message == "The original PDF has not been uploaded yet" || result.Message == "The XML has not been uploaded yet" || result.Message == "Please check the files uploaded")
                                    {
                                        return BadRequest(result);
                                    }
                                    else
                                    {
                                        result = TSR_DAO.RollBackDocumentAdmin(doc, currentUser.Uid, currentUser.DynamicToken!);
                                        await _hubContext.Clients.All.SendAsync("RecordUpdated");
                                        await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                                        return BadRequest(result);
                                    }
                                }
                                else
                                {
                                    result.Message = "Something went wrong";
                                    result.State = 1;
                                    return BadRequest(result);
                                }
                            }
                        }
                        else if(doc.DocumentType == 7)
                        {
                            if(Path.GetExtension(doc.DocumentFile.FileName)!= ".xml")
                            {
                                result.State = 1;
                                result.Message = "Este documento no es formato XML";
                                return Ok(result);
                            }
                            else
                            {
                                var filePath = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType) + "/" + doc.DocumentFile.FileName);

                                var removeFile = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType));

                                result = TSR_DAO.UploadDocumentAdmin(doc, currentUser.Uid, filePath, doc.DocumentFile.FileName, currentUser.DynamicToken!);

                                if (result.State == 0)
                                {
                                    if (Directory.Exists(removeFile)) Directory.Delete(removeFile, true);

                                    new FileInfo(filePath).Directory?.Create();

                                    await using (var stream = new FileStream(filePath, FileMode.Create))
                                    {
                                        _logger.LogInformation($"Saving file [{doc.DocumentFile.FileName}]");
                                        await doc.DocumentFile.CopyToAsync(stream);
                                        _logger.LogInformation($"\t The uploaded file is saved as [{filePath}]");
                                    }
                                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                                    await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                                    return Ok(result);
                                }
                                else
                                {
                                    if (result.Message!.Length > 0)
                                    {
                                        if (result.Message == "El layout no ha sido aceptado o el TMW no ha sido agregado o el num. cp no se ha agregado o esta solicitud ha sido completada" || result.Message == "File already uploaded" || result.Message == "The TMW must be added" || result.Message == "The layout has not been uploaded yet" || result.Message == "The consignment note has not been added yet" || result.Message == "The original PDF has not been uploaded yet" || result.Message == "The XML has not been uploaded yet")
                                        {
                                            return BadRequest(result);
                                        }
                                        else
                                        {
                                            result = TSR_DAO.RollBackDocumentAdmin(doc, currentUser.Uid, currentUser.DynamicToken!);
                                            await _hubContext.Clients.All.SendAsync("RecordUpdated");
                                            await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                                            return BadRequest(result);
                                        }
                                    }
                                    else
                                    {
                                        result.Message = "Something went wrong";
                                        result.State = 1;
                                        return BadRequest(result);
                                    }
                                }
                            }
                        }
                        else
                        {
                            result.Message = "The document type is not allowed";
                            result.State = 1;
                            return BadRequest(result);
                        }
                    }
                }
                else
                {
                    result.Message = "Please verify the information added";
                    result.State = 1;
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateLayoutStatus")]
        [Authorize]
        public async Task<IActionResult> UpdateLayouStatus(UpdateLayoutStatus updateLayoutStatus)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateLayoutStatus(updateLayoutStatus, currentUser.Uid, currentUser.DynamicToken!);
                if(result.State == 1)
                {
                    return BadRequest(result);
                }
                else if (result.State == 401)
                {
                    return BadRequest(result);
                }
                else
                {
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion

        #region customer-procedures
        [HttpGet("GetDocuments")]
        [Authorize]
        public IActionResult GetDocuments(int id)
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<Document> resDAO = TSR_DAO.GetDocuments(currentUser.Uid, id, currentUser.DynamicToken!);
                if (resDAO[0].State == 1)
                {
                    result = Helpers.BuildResult(resDAO[0].State, resDAO[0].Message!, true);
                    return BadRequest(result);
                }
                else if (resDAO[0].State == 401)
                {
                    result = Helpers.BuildResult(resDAO[0].State, resDAO[0].Message!, true);
                    return BadRequest(result);
                }
                else
                {
                    return Ok(resDAO);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UploadDocumentCustomer")]
        [Authorize]
        public async Task<IActionResult> UploadDocumentCustomer([FromForm] UploadFile doc)
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                if (doc.DocumentId > 0 && doc.ServiceRequestId > 0 && doc.DocumentType > 0)
                {
                    if(doc.DocumentFile == null && doc.DocumentFile!.Length > 0)
                    {
                        result.Message = "The file is empty please check";
                        result.State = 1;
                        return BadRequest(result);
                    }
                    else
                    {
                        if(doc.DocumentType == 1 || doc.DocumentType == 2 || doc.DocumentType == 3 || doc.DocumentType == 4 || doc.DocumentType == 6)
                        {
                            var filePath = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType) + "/" + doc.DocumentFile.FileName);

                            var removeFile = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(doc.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(doc.DocumentId) + "/File-" + Convert.ToString(doc.DocumentType));

                            result = TSR_DAO.UploadDocumentCustomer(doc, currentUser.Uid, filePath, doc.DocumentFile.FileName, currentUser.DynamicToken);

                            if(result.State == 0)
                            {
                                if(Directory.Exists(removeFile)) Directory.Delete(removeFile, true);

                                new FileInfo(filePath).Directory?.Create();
                                await using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    _logger.LogInformation($"Saving file [{doc.DocumentFile.FileName}]");
                                    await doc.DocumentFile.CopyToAsync(stream);
                                    _logger.LogInformation($"\t The uploaded file is saved as [{filePath}]");
                                }
                                await _hubContext.Clients.All.SendAsync("RecordUpdated");
                                await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                                return Ok(result);
                            }
                            else
                            {
                                if(result.Message!.Length > 0)
                                {
                                    if(result.Message == "File already uploaded")
                                    {
                                        return BadRequest(result);
                                    }
                                    else
                                    {
                                        result = TSR_DAO.RollBackDocumentCustomer(doc, currentUser.Uid, currentUser.DynamicToken);
                                        await _hubContext.Clients.All.SendAsync("RecordUpdated");
                                        await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                                        return BadRequest(result);
                                    }
                                }
                                else
                                {
                                    result.Message = "Something went wrong";
                                    result.State = 1;
                                    return BadRequest(result);
                                }
                            }
                        }
                        else
                        {
                            result.Message = "The document type is not allowed";
                            result.State = 1;
                            return BadRequest(result);
                        }
                    }
                }
                else
                {
                    result.Message = "Please verify the information added";
                    result.State = 1;
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion

        [HttpPost("DownloadFile")]
        [Authorize]
        public async Task<ActionResult> DownloadFile(DownloadFile df)
        {
            // validation and get the file
            Result result = new Result();
            //var filePath = $"{id}.txt";
            var filePath = Path.Combine(df.Url);

            if (!System.IO.File.Exists(filePath))
            {
                result.Message = "El archivo no existe";
                result.State = 1;
                return Ok(result);
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var filname = Path.GetFileName(filePath);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        [HttpPost("RemoveFileAdmin")]
        [Authorize]
        public async Task<IActionResult> RemoveFileAdmin(RemoveFile rf)
        {
            var currentUser = GetCurrentUser();
            Result result = TSR_DAO.RemoveFileAdmin(rf, currentUser.Uid, currentUser.DynamicToken!);
            if (result.State == 0)
            {

                //var filePath = Path.Combine(@"D:/tsr/App_Data/Requests/" + "SR-" + Convert.ToString(rf.ServiceRequest_Id) + "/Documents/" + "DC-" + Convert.ToString(rf.Document_Id) + "/File-" + Convert.ToString(rf.Document_Type) + "/" + rf.FileName);
                //local
                var filePath = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(rf.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(rf.DocumentId) + "/File-" + Convert.ToString(rf.DocumentType) + "/" + rf.FileName);
                System.IO.File.Delete(filePath);
                await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                return Ok(result);
            }
            else if (result.State == 401)
            {
                return BadRequest(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("RemoveFileCustomer")]
        [Authorize]
        public async Task<IActionResult> RemoveFileCustomer(RemoveFile rf)
        {
            var currentUser = GetCurrentUser();
            Result result = TSR_DAO.RemoveFileCustomer(rf, currentUser.Uid, currentUser.DynamicToken!);
            if (result.State == 0)
            {

                //var filePath = Path.Combine(@"D:/tsr/App_Data/Requests/" + "SR-" + Convert.ToString(rf.ServiceRequest_Id) + "/Documents/" + "DC-" + Convert.ToString(rf.Document_Id) + "/File-" + Convert.ToString(rf.Document_Type) + "/" + rf.FileName);
                //local
                var filePath = Path.Combine(@"App_Data/Requests/" + "SR-" + Convert.ToString(rf.ServiceRequestId) + "/Documents/" + "DC-" + Convert.ToString(rf.DocumentId) + "/File-" + Convert.ToString(rf.DocumentType) + "/" + rf.FileName);
                System.IO.File.Delete(filePath);
                await _hubContext.Clients.All.SendAsync("RecordUpdated");
                await _hubContext.Clients.All.SendAsync("RecordDocumentUpdated");
                return Ok(result);
            }
            else if (result.State == 401)
            {
                return BadRequest(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        private UserModelLogin GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModelLogin
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == "Username")?.Value,
                    CustomerName = userClaims.FirstOrDefault(o => o.Type == "UserEmail")?.Value,
                    DynamicToken = userClaims.FirstOrDefault(o => o.Type == "DynamicToken")?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == "UserGivenName")?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == "UserSurname")?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == "UserRole")?.Value,
                    Uid = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == "Uid")?.Value),
                };
            }
            return null!;
        }
    }
}
