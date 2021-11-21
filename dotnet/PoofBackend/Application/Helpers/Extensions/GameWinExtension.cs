using Application.Exceptions;
using Application.Models.GameModels.WinModels;
using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
