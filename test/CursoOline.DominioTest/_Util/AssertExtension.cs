using Xunit;

namespace CursoOline.DominioTest._Util;

public static class AssertExtension
{
    public static void ComMensagem(this ArgumentException exception, string mensagem)
    {
        if (exception.Message == mensagem)
        {
            Assert.True(true);
        }
        else
        {
            Assert.True(false, $"Esperava a mensagem: {mensagem}");
        }
    }
}