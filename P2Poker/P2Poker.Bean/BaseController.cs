using Newtonsoft.Json;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Singletons;

namespace P2Poker.Bean;

public abstract class BaseController
{
    protected RequestCode requestCode = RequestCode.None;

    public RequestCode RequestCode
        =>requestCode;
}