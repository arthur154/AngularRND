using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularApp.Models.Search;
using System.IO;
using Newtonsoft.Json;
using AngularApp.Models.Member;
using System.Net.Http;
using System.Text;
using AngularApp.Utility;

namespace AngularApp.Controllers
{
    [Route("api/[controller]")]
    public class MemberController : BaseController
    {
        [HttpGet]
        public ActionResult Get()
        {
            var members = JsonConvert.DeserializeObject<List<Member>>(RestClient.Get(ApiEndpoint + "/api/member"));
            if (members.Count() > 0)
            {
                return Json(members[0]);
            }
            throw new Exception("No member was found!");
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var member = JsonConvert.DeserializeObject<Member>(RestClient.Get(string.Format(ApiEndpoint + "/api/member/{0}", id)));
            return Json(member);
        }

        [HttpGet("[action]")]
        public ActionResult List()
        {
            return Json(RestClient.Get("/api/member"));
        }

        [HttpPost("[action]")]
        public ActionResult Search([FromBody] SearchRequest request)
        {
            var members = JsonConvert.DeserializeObject<List<Member>>(RestClient.Get(ApiEndpoint + "/api/member"));
            var result = new SearchResults()
            {
                data = members.ToArray(),
                draw = request.draw,
                recordsFiltered = members.Count,
                recordsTotal = members.Count
            };
            return Json(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Member member)
        {
            //return Json("{\"correlationid\":\"85895510-f71b-4126-ae41-10dd4be886f5\"}");
            member.PersonGUID = Guid.NewGuid().ToString();
            return Json(RestClient.Post(ApiEndpoint + "/api/incoming/insert", member));
        }

        [HttpGet("[action]/{id}")]
        public ActionResult Status(string id)
        {
            return Json(RestClient.Get(string.Format(ApiEndpoint + "/api/status/{0}", id)));
        }
    }
}