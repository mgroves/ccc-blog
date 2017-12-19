using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using swashbuckle_example.Models;

namespace swashbuckle_example.Controllers
{
    // tag::ValuesController[]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Route("api/teams")]
        public IActionResult GetTeams()
        {
            var jsonFile = System.IO.File.ReadAllText("jsonFile.json");
            var teams = JsonConvert.DeserializeObject<List<Team>>(jsonFile);
            return Ok(teams);
        }

        [HttpPost]
        [Route("api/team")]
        public IActionResult PostTeam([FromBody]Team team)
        {
            var jsonFile = System.IO.File.ReadAllText("jsonFile.json");
            var teams = JsonConvert.DeserializeObject<List<Team>>(jsonFile);
            teams.Add(team);
            System.IO.File.WriteAllText("jsonFile.json",JsonConvert.SerializeObject(teams));
            return Ok(team);
        }

        // etc...

        // end::ValuesController[]

        // tag::GetTeams2[]
        /// <summary>
        /// Gets all the teams stored in the file
        /// </summary>
        /// <remarks>Baseball is the best sport</remarks>
        /// <response code="200">List returned succesfully</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet]
        [Route("api/teams2")]
        [ProducesResponseType(typeof(Team), 200)]
        public IActionResult GetTeams2()
        {
            var jsonFile = System.IO.File.ReadAllText("jsonFile.json");
            var teams = JsonConvert.DeserializeObject<List<Team>>(jsonFile);
            return Ok(teams);
        }
        // end::GetTeams2[]
    }
}
