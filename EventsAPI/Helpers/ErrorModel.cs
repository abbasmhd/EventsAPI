using System;

namespace EventsAPI.Helpers
{

    public class ErrorModel
    {

        public ErrorModel() { }

        public ErrorModel(string field, string message) {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }
        public string Message { get; set; }
    }

}
