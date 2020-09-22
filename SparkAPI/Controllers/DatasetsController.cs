using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkAPI.Data;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatasetsController : ControllerBase
    {

        private readonly SparkAPIContext _context;

        public DatasetsController(SparkAPIContext context)
        {
            _context = context;
        }

        // GET: api/Datasets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dataset>>> GetDataset(string path1, string path2)
        {
            ProcessStartInfo infos = new ProcessStartInfo();
            infos.FileName = "cmd.exe";
            infos.Arguments = @"/c cd ..\GettingData\bin\Debug\netcoreapp3.1 && dotnet build ..\..\..\GettingData.csproj && spark-submit --class org.apache.spark.deploy.dotnet.DotnetRunner --master local microsoft-spark-2.4.x-0.12.1.jar dotnet GettingData.dll " + path1 + " "+ path2;

            Process cmd = new Process();
            cmd.StartInfo = infos;

            cmd.Start();

            return await _context.Dataset.ToListAsync();
        }

        // GET: api/Datasets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dataset>> GetDataset(long id)
        {
            var dataset = await _context.Dataset.FindAsync(id);

            if (dataset == null)
            {
                return NotFound();
            }

            return dataset;
        }

        // PUT: api/Datasets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataset(long id, Dataset dataset)
        {
            if (id != dataset.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatasetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Datasets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dataset>> PostDataset(Dataset dataset)
        {
            _context.Dataset.Add(dataset);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataset", new { id = dataset.Id }, dataset);
        }

        // DELETE: api/Datasets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dataset>> DeleteDataset(long id)
        {
            var dataset = await _context.Dataset.FindAsync(id);
            if (dataset == null)
            {
                return NotFound();
            }

            _context.Dataset.Remove(dataset);
            await _context.SaveChangesAsync();

            return dataset;
        }

        private bool DatasetExists(long id)
        {
            return _context.Dataset.Any(e => e.Id == id);
        }
    }
}
