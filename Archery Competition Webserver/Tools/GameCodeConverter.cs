using Org.BouncyCastle.Asn1.Mozilla;

namespace Archery_Competition_Webserver.Tools
{
    public enum GameMode
    {
        Qualification,
        MatchPlay,
        RoundRobin,
        SwissSystem
    }

    /// <summary>
    /// Class to sign unique game among a competition.
    /// </summary>
    public class GameInfo
    {
        private GameMode mode;
        private int? stage;
        private int? matchup;
        public GameMode Mode { get { return mode; } set { mode = value; } }
        public int? Stage { get { return stage; } set { stage = value; } }
        public int? Matchup { get { return matchup; } set { matchup = value; } }

        public GameInfo()
        {
            Mode = GameMode.Qualification;
            Stage = null;
            Matchup = null;
        }

    }

    public static class GameCodeConverter
    {
        public static int GameEncoder(GameInfo gameInfo)
        {
            string mode = gameInfo.Mode.ToString("D1");
            string stage = gameInfo.Stage == null ? "00": ((int)gameInfo.Stage).ToString("D2");
            string matchup = gameInfo.Matchup == null ? "00000": ((int)gameInfo.Matchup).ToString("D5");
            string encodedstr = $"{mode}{stage}{matchup}";

            return int.Parse(encodedstr) ;
        }

        public static GameInfo GameDecoder(int gameCode)
        {
            int matchup = gameCode % 100000;
            gameCode = gameCode / 100000;
            int stage = gameCode % 100;
            gameCode = gameCode / 100;
            GameMode mode =(GameMode) Enum.ToObject(typeof(GameMode),(byte) gameCode);

            GameInfo gameInfo = new GameInfo();
            gameInfo.Mode = mode;
            gameInfo.Stage = stage;
            gameInfo.Matchup = matchup;

            return gameInfo ;
        }
    }
}
