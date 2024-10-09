using Microsoft.AspNetCore.Mvc;

namespace APILivraria.Controllers;
[Route("livraria/[controller]")]
[ApiController]
public class LivrariaController : ControllerBase
{
    
    private static List<Livro> livros = new List<Livro>
        {
            new Livro 
            { 
                Id = 1, 
                Titulo = "O Príncipe Cruel", 
                Autor = "Holly Black", 
                Genero = Genero.Fantasia.ToString(), 
                Preco = 25.50M, 
                Estoque = 10 
            },

            new Livro

            {
                Id = 2,
                Titulo = "A Hipóste do Amor",
                Autor = "Ale Hazelwood",
                Genero = Genero.Romance.ToString(),
                Preco = 45M,
                Estoque = 5
            },
    };

    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Livro>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(livros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var livro = livros.FirstOrDefault(l => l.Id == id);
        if (livro == null)
        {
            return NotFound("Livro não encontrado.");
        }

        return Ok(livro);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Livro request)
    {
        if (request == null)
        {
            return BadRequest("Livro inválido.");
        }

        request.Id = livros.Count + 1; 
        livros.Add(request);

        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult Update(int id, [FromBody] Livro request)

    { if (request == null)
        {
            return BadRequest("Update inválido.");
        }

        var livroExistente = livros.FirstOrDefault(l => l.Id == id);
        if (livroExistente == null)
        {
            return NotFound("Livro não encontrado.");
        }

        livroExistente.Titulo = request.Titulo;
        livroExistente.Autor = request.Autor;
        livroExistente.Genero = request.Genero;
        livroExistente.Preco = request.Preco;
        livroExistente.Estoque = request.Estoque;

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var livroExistente = livros.FirstOrDefault(l => l.Id == id);
        if (livroExistente == null)
        {
            return NotFound("Livro não encontrado.");
        }

        livros.Remove(livroExistente);

        return NoContent();
    }
}
