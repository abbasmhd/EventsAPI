using System.Collections.Generic;

namespace EventsAPI.Helpers {

    public class ErrorResponse
    {
        public IList<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }

}
