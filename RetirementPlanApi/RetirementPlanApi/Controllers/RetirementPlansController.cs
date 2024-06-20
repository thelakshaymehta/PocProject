using RetirementPlanApi.Models;
using RetirementPlanApi.Repositories;
using System.Web.Http;

public class RetirementPlansController : ApiController
{
    private readonly RetirementPlanRepository _repository = new RetirementPlanRepository();

    [HttpGet]
    public IHttpActionResult Get()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet]
    public IHttpActionResult Get(int id)
    {
        var plan = _repository.GetById(id);
        if (plan == null)
        {
            return NotFound();
        }
        return Ok(plan);
    }

    [HttpPost]
    public IHttpActionResult Post([FromBody] RetirementPlan plan)
    {
        _repository.Create(plan);
        return CreatedAtRoute("DefaultApi", new { id = plan.Id }, plan);
    }

    [HttpPut]
    public IHttpActionResult Put(int id, [FromBody] RetirementPlan plan)
    {
        if (id != plan.Id)
        {
            return BadRequest();
        }
        _repository.Update(plan);
        return StatusCode(System.Net.HttpStatusCode.NoContent);
    }

    [HttpDelete]
    public IHttpActionResult Delete(int id)
    {
        _repository.Delete(id);
        return StatusCode(System.Net.HttpStatusCode.NoContent);
    }
}
