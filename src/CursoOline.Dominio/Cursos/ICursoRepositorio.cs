using CursoOline.DominioTest.Cursos;

namespace CursoOline.Dominio.Cursos;

public interface ICursoRepositorio
{
    void Adicionar(Curso curso);
    Curso ObterPeloNome(string nome);
}