public class Counter
{
    public static int CountBall { get; set; }
    public static int MaxCountBall { get; set; } = 1;
    public static bool Overboard { get; set; }

    public static float BestTime3 { get; set; } = 0;
    public static float BestTime5 { get; set; } = 0;

    public static float TimeCount { get; set; } = 0;
    public static bool BeginStart { get; set; }
    public static bool In_Internet { get; set; } = false;
    public static bool In_Timer { get; set; } = false;

    public static bool BeginWin { get; set; }
    public static string UserName { get; set; }
    public static string UserMail { get; set; }
}

public class User
{
    public string name;
    public string mail;
    public int balls;
    public float time;

    public User(string name, string mail, int balls, float time)
    {
        this.name = name;
        this.mail = mail;
        this.balls = balls;
        this.time = time;
    }
}

public class Admin
{
    public string data;
    public float time;
    public Admin(string data, float time)
    {
        this.data = data;
        this.time = time;
    }

}

