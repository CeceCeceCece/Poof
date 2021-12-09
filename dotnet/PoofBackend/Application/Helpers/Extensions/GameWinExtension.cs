using Application.Exceptions;
using Application.Models.GameModels.WinModels;

namespace Domain.Constants.Enums
{
    public static class GameWinExtension
    {
        public static IWinLogic GetWinLogic(this WinType type) => type switch
        {
            WinType.BasicBang => new BasicWinLogic(),
            _ => throw new PoofException("Ilyen győzelmi feltétel nem létezik!")
        };
    }
}
