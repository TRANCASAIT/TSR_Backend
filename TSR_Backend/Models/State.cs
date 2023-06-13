namespace TSR_Backend.Models
{
    public class State
    {
        public string? StateName { get; set; }

        public class StateUpdate: State
        {
            public int StateId { get; set; }
        }
        public class StateGet: StateUpdate
        {

        }
    }
}
