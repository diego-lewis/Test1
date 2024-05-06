using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Test1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string Message { get; set; }
        public void OnGet()
        {
            // Message = string.Empty;
            string result = string.Empty;
            if (Request.Query.Keys.Contains("handinput"))
            {
                Hand myHand = new Hand();
                if (myHand.getHand(Request.Query["handinput"]))
                {
                    result = myHand.readHand();
                }
                else
                {
                    result = "Could not input your hand.";
                }
            }
            Message = result;
        }

        public void OnPost()
        {
            string result = string.Empty;
            if (Request.Form.ContainsKey("handinput"))
            {
                Hand myHand = new Hand();
                if (myHand.getHand(Request.Form["handinput"]))
                {
                    result = myHand.readHand();
                }
                else
                {
                    result = "Could not input your hand.";
                }
            }
            Message = result;
        }
    }
}
