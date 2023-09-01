using Bogus;
using CursoOline.Dominio.Cursos;
using CursoOline.DominioTest._Util;
using CursoOline.DominioTest.Builders;
using Moq;
using Xunit;

namespace CursoOline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    private readonly CursoDto _cursoDto;
    private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
    private readonly ArmazenadorDeCurso _armazenadorDeCurso;

    public ArmazenadorDeCursoTest()
    {
        var fake = new Faker();

        _cursoDto = new CursoDto
        {
            Nome = fake.Random.Word(),
            CargaHoraria = fake.Random.Double(50, 1000),
            PublicoAlvo = "Estudante",
            Valor = fake.Random.Double(100, 1000),
            Descricao = fake.Lorem.Paragraph()
        };

        _cursoRepositorioMock = new Mock<ICursoRepositorio>();
        _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
    }

    [Fact]
    public void DeveAdicionarCurso()
    {
        _armazenadorDeCurso.Armazenar(_cursoDto);

        _cursoRepositorioMock.Verify(r => r.Adicionar(
            It.Is<Curso>(
                c => c.Nome == _cursoDto.Nome &&
                    c.Descricao == _cursoDto.Descricao &&
                    c.CargaHoraria == _cursoDto.CargaHoraria &&
                    c.Valor == _cursoDto.Valor
                )
            ));
    }

    [Fact]
    public void NaoDeveInformarComPublicoAlvoInvalido()
    {
        _cursoDto.PublicoAlvo = "Medico";
        Assert.Throws<ArgumentException>( () => _armazenadorDeCurso.Armazenar(_cursoDto))
            .ComMensagem("Público Alvo inválido!");
    }

    [Fact]
    public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
    {
        var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
        _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

        Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
            .ComMensagem("Nome do curso já conta no DB!");
    }
}