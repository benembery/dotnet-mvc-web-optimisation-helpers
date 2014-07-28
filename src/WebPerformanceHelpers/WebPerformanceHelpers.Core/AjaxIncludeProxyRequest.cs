using System.Collections.Generic;
using System.Linq;

namespace WebPerformanceHelpers.Core
{
    public class AjaxIncludeProxyRequest
    {
        public bool Wrap { get; set; }
        public string Includes { get; set; }

        private IList<string> _requests;
        public IList<string> RequestsList
        {
            get
            {
                if (_requests != null && _requests.Any())
                    return _requests;

                if (string.IsNullOrWhiteSpace(Includes))
                    return new List<string>();

                _requests = Includes.Split(',').ToList();

                return _requests;
            }
            set { _requests = value; }
        }
    }
}