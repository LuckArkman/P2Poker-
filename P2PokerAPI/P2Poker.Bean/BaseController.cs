using P2Poker.Enums;

namespace P2Poker.Bean;

public abstract class BaseController
{
    protected RequestCode requestCode = RequestCode.None;

    public RequestCode RequestCode
        =>requestCode;
}