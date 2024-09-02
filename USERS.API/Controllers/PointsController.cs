using Microsoft.AspNetCore.Mvc;
using USERS.API.Models;

namespace USERS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsController : ControllerBase
    {
        public IEnumerable<Point> points { get; private set; } = new List<Point>()
        {
            new Point(1, "Juca", "Descrição do ponto 1", 100),
            new Point(2, "Joca", "Descrição do ponto 2", 200 ),
            new Point(3, "Joca", "Descrição do ponto 3", 300 )
        };

        [HttpGet]
        public IEnumerable<Point> Get()
        {
            return points;
        }

        [HttpGet("{id}")]
        public Point GetPointById(int id)
        {
            return points.FirstOrDefault(x => x.Id == id);
        }


        [HttpPost]
        public IEnumerable<Point> Post(Point point)
        {
            return points.Append<Point>(point);
        }

        [HttpPut("{id}")]
        public IEnumerable<Point> Put(Point point)
        {
            return points.Select(x => x.Id == point.Id ? point : x);
        }

    }
}