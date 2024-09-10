using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.DTO.Response
{
    public class BaseResponse<T>
    {
        public bool Success
        {
            get
            {
                return Errors == null || Errors.Count > 0 == false;
            }
        }

        public T Data { get; set; }

        public IReadOnlyCollection<ErrorResponse> Errors { get; set; }
    }
}
