using Bogus;
using CursoOline.DominioTest._Util;
using ExpectedObjects;
using Xunit;
using CursoOline.DominioTest.Builders;

namespace CursoOline.DominioTest.Cursos
{
    public class CursoTest
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;
        private readonly Faker _faker;

        public CursoTest()
        {
            _faker = new Faker();

            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() =>
                    CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome inválido!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() => 
                    CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga Horária inválida!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() => 
                 CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor inválido!");
        }
    }
}