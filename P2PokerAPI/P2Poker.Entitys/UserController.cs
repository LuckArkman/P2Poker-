using P2Poker.Bean;
using P2Poker.Enums;

namespace P2Poker.Entitys;

public class UserController:BaseController
{
    //private UserDAO userDAO=new UserDAO();
    //private ResultDAO resultDAO = new ResultDAO();
    public UserController()
    {
        requestCode =  RequestCode.User;
    }

    public string Login(string data,Client client,Server server)
    {
        /*
        string[] strs = data.Split(',');
        User user= userDAO.VerifyUser(client.MysqlConn, strs[0], strs[1]);
        if (user == null)
        {
            return ((int)ReturnCode.Fail).ToString();
        }
        else
        {
            Result res = resultDAO.GetResultByUserId(client.MysqlConn, user.Id);
            client.SetUserData(user, res);
            return string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, res.TotalCount, res.WinCount);
        }
        */
        return "";
    }
    
    public string Register(string data, Client client, Server server)
    {
        /*
        string[] strs = data.Split(',');
        string username = strs[0];
        string password = strs[1];
        bool res = userDAO.GetUserByUsername(client.MysqlConn, username);
        if (res)
        {
            return ((int)ReturnCode.Fail).ToString();
        }
        userDAO.AddUser(client.MysqlConn, username, password);
        return ((int)ReturnCode.Success).ToString();
        */
        return "";
    }
}