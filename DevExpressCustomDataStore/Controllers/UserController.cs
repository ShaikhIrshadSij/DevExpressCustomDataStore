using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevExpressCustomDataStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetData")]
        public IActionResult GetData(int skip, int take, string sort, string filter, string group, bool requireTotalCount, bool requireGroupCount)
        {
            DataSourceLoadOptionsBase loadOptions = new DataSourceLoadOptionsBase();
            if (!string.IsNullOrWhiteSpace(sort))
                loadOptions.Sort = JsonConvert.DeserializeObject<SortingInfo[]>(sort);

            if (!string.IsNullOrWhiteSpace(filter))
                loadOptions.Filter = JsonConvert.DeserializeObject<IList>(filter);

            if (!string.IsNullOrWhiteSpace(group))
                loadOptions.Group = JsonConvert.DeserializeObject<GroupingInfo[]>(group);

            List<DummyData> dummyData = new List<DummyData>();
            for (int i = 0; i < 100; i++)
            {
                dummyData.Add(new DummyData
                {
                    Email = $"useremail{i}@gmail.com",
                    FirstName = $"User First Name {i}",
                    LastName = $"User Last Name {i}",
                    Counter = i
                });
            }

            loadOptions.Skip = skip;
            loadOptions.Take = take;
            loadOptions.RequireTotalCount = requireTotalCount;
            loadOptions.RequireGroupCount = requireGroupCount;
            var listData = DataSourceLoader.Load(dummyData, loadOptions);
            return Ok(listData);
        }
    }

    public class DummyData
    {
        public int Counter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}