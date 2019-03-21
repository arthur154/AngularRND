using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularApp.Models.DataTableExample;
using AngularApp.Models.Search;
using System.IO;
using Newtonsoft.Json;

namespace AngularApp.Controllers
{
    [Route("api/[controller]")]
    public class DataTableExampleController : BaseController
    {
        [HttpGet("[action]")]
        public ActionResult GetDataManually()
        {
            return Json(CreateFakeData());
        }

        [HttpPost("[action]")]
        public ActionResult GetData([FromBody] SearchRequest request)
        {
            return Json(CreateFakeData(request));
        }

        // TODO: Remove this nastyness (for demoing data only)
        private SearchResults CreateFakeData(SearchRequest request = null)
        {
            List<Person> data;
            int originalLength;
            FileStream fileStream = new FileStream("Content/dataTablesExampleData.json", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string jsonData = reader.ReadLine();
                data = JsonConvert.DeserializeObject<List<Person>>(jsonData);
                originalLength = data.Count();
            }

            if (request != null)
            {
                // Search filter
                var searchString = request.search.value.ToUpper();
                data = data.Where(p => p.id.ToString().Contains(searchString) || p.firstName.ToUpper().Contains(searchString) || p.lastName.ToUpper().Contains(searchString)).ToList();

                // Data ordering
                if (request.order.Count() > 0)
                {
                    if (request.order[0].dir == "asc")
                    {
                        switch (request.order[0].column)
                        {
                            case 0:
                                data = data.OrderBy(p => p.id).ToList();
                                break;
                            case 1:
                                data = data.OrderBy(p => p.firstName).ToList();
                                break;
                            default:
                                data = data.OrderBy(p => p.lastName).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (request.order[0].column)
                        {
                            case 0:
                                data = data.OrderByDescending(p => p.id).ToList();
                                break;
                            case 1:
                                data = data.OrderByDescending(p => p.firstName).ToList();
                                break;
                            default:
                                data = data.OrderByDescending(p => p.lastName).ToList();
                                break;
                        }
                    }
                }

                // Trim results
                if (data.Count() > request.start + request.length)
                {
                    data = data.GetRange(request.start, request.length);
                }
            }

            var results = new SearchResults()
            {
                data = data.ToArray(),
                recordsTotal = data.Count(),
                recordsFiltered = originalLength - data.Count(),
                draw = 4
            };

            return results;
        }
    }
}
