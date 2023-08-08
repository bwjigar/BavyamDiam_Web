using Lib.Models;
using SunriseWeb.Data;
using SunriseWeb.Helper;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunriseWeb.Controllers
{
    public class DNAController : Controller
    {
        API _api = new API();
        public ActionResult Index(string StoneNo)
        {
            string inputJson = string.Empty;
            int iUserId = 0;
            if (Request.Cookies["Userid_DNA"] != null)
            {
                iUserId = Convert.ToInt32(Request.Cookies["Userid_DNA"].Value);
            }
            var input = new
            {
                StoneID = StoneNo,
                UserId = iUserId
            };
            inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetSearchStockByStoneIDWithoutToken, inputJson);
            SearchStone _data1 = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return View(_data1);
        }
        public ActionResult StoneDetail(string StoneNo)
        {
            string inputJson = string.Empty;
            int iUserId = 0;
            if (Request.Cookies["Userid_DNA"] != null)
            {
                iUserId = Convert.ToInt32(Request.Cookies["Userid_DNA"].Value);
            }
            var input = new
            {
                StoneID = StoneNo,
                UserId = iUserId
            };
            inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetSearchStockByStoneIDWithoutToken, inputJson);
            SearchStone _data1 = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return View(_data1);
        }
        public ActionResult NewStoneDetail(string StoneNo)
        {
            string inputJson = string.Empty;
            int iUserId = 0;
            if (Request.Cookies["Userid_DNA"] != null)
            {
                iUserId = Convert.ToInt32(Request.Cookies["Userid_DNA"].Value);
            }
            var input = new
            {
                StoneID = StoneNo,
                UserId = iUserId
            };
            inputJson = (new JavaScriptSerializer()).Serialize(input);
            string _response = _api.CallAPIWithoutToken(Constants.GetSearchStockByStoneIDWithoutToken, inputJson);
            SearchStone _data1 = (new JavaScriptSerializer()).Deserialize<SearchStone>(_response);
            return View(_data1);
        }
    }
}