// Global Chat v1.4.0
// Author: Felladrin
// Started: 2013-07-03
// Updated: 2016-01-06

using System.Collections.Generic;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Felladrin.Engines
{
    public static class GlobalChat
    {
        public static class Config
        {
            public static bool Enabled = true;               // Is this system enabled?
            public static bool OpenHistoryOnLogin = true;    // Should we display the history when player logs in?
            public static bool AutoColoredNames = true;      // Should we auto color the players names?
            public static int HistorySize = 70;              // How many messages should we keep in the history?
            public static int MessageHue = 1154;             // What is the hue of the chat messages?
        }

        public static void Initialize()
        {
            if (Config.Enabled)
            {
                CommandSystem.Register("ChatToggle", AccessLevel.Player, new CommandEventHandler(OnCommandToggle));
                CommandSystem.Register("ChatHistory", AccessLevel.Player, new CommandEventHandler(OnCommandHistory));
                CommandSystem.Register("C", AccessLevel.Player, new CommandEventHandler(OnCommandChat));
                 CommandSystem.Register("Chat", AccessLevel.Player, new CommandEventHandler(OnCommandChat));
                EventSink.Login += OnLogin;
            }
        }

        static readonly List<string> History = new List<string>();

        static HashSet<int> DisabledPlayers = new HashSet<int>();

        [Usage("ChatToggle")]
        [Description("Enables or Disables the Chat.")]
        static void OnCommandToggle(CommandEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;
            var acc = pm.Account as Account;

            if (acc.GetTag("Chat") == null || acc.GetTag("Chat") == "Enabled")
            {
                DisabledPlayers.Add(pm.Serial.Value);
                acc.SetTag("Chat", "Disabled");
                pm.SendMessage(38, "You have disabled the chat for your account.");

                if (pm.HasGump(typeof(ChatHistoryGump)))
                    pm.CloseGump(typeof(ChatHistoryGump));
            }
            else
            {
                DisabledPlayers.Remove(pm.Serial.Value);
                acc.SetTag("Chat", "Enabled");
                pm.SendMessage(68, "You have enabled the chat for your account.");
                pm.SendGump(new ChatHistoryGump());
            }
        }

        [Usage("ChatHistory")]
        [Description("Opens the Chat History.")]
        static void OnCommandHistory(CommandEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;

            if (DisabledPlayers.Contains(pm.Serial.Value))
            {
                pm.SendMessage(38, "Chat is currently disabled for your account. Type [ChatToggle to enable it.");
                return;
            }

            if (pm.HasGump(typeof(ChatHistoryGump)))
                pm.CloseGump(typeof(ChatHistoryGump));

            pm.SendGump(new ChatHistoryGump());
        }

        [Usage("C <message>")]
        [Description("Broadcasts a message to all players online. If no message is provided, it opens the Chat History.")]
        static void OnCommandChat(CommandEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;

            if (DisabledPlayers.Contains(pm.Serial.Value))
            {
                pm.SendMessage(38, "Chat is currently disabled for your account. Type [ChatToggle to enable it.");
                return;
            }

            if (e.ArgString.Length == 0)
            {
                if (pm.HasGump(typeof(ChatHistoryGump)))
                    pm.CloseGump(typeof(ChatHistoryGump));

                pm.SendGump(new ChatHistoryGump());
            }
            else
            {
                Broadcast(e.Mobile, e.ArgString);
            }
        }

        static void OnLogin(LoginEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;
            var acc = pm.Account as Account;

            if (acc.GetTag("Chat") == "Disabled")
            {
                DisabledPlayers.Add(pm.Serial.Value);
                pm.SendMessage("Chat is Disabled for your account.");
            }
            else
            {
                pm.SendMessage("Chat is Enabled for your account.");

                if (Config.OpenHistoryOnLogin)
                    pm.SendGump(new ChatHistoryGump());
            }
        }

        static void Broadcast(Mobile sender, string message)
        {
            if (History.Count > Config.HistorySize)
                History.RemoveAt(0);

            History.Add(string.Format("<basefont size=3 color=#524759>[{0}] <basefont size=3 color=#{1}>{2}<basefont color=#14131A> {3}", System.DateTime.UtcNow.ToString("<basefont size=3 color=#151524>HH:mm"), (Config.AutoColoredNames ? (sender.Name.GetHashCode()>>8).ToString() : "54432D"), sender.Name, Utility.FixHtml(message)));

            foreach (NetState ns in NetState.Instances)
            {
                var player = ns.Mobile as PlayerMobile;

                if (player == null || DisabledPlayers.Contains(player.Serial.Value))
                    continue;

                player.SendMessage(Config.MessageHue, string.Format("<{0}> {1}", sender.Name, message));

                if (player.HasGump(typeof(ChatHistoryGump)))
                {
                    player.CloseGump(typeof(ChatHistoryGump));
                    player.SendGump(new ChatHistoryGump());
                }
            }
        }

        static string GenerateHistoryHTML()
        {
            if (History.Count == 0)
                return "<basefont size=3 color=#151524>Welcome to the Dark Forest! Say hi!";

            string HTML = "";

            foreach (string msg in History)
                HTML = msg + " <br/>" + HTML;

            return HTML;
        }

        public class ChatHistoryGump : Gump
        {
            public ChatHistoryGump() : base(110, 100)
            {
                Closable = true;
                Dragable = true;
                Disposable = true;
                Resizable = false;

                AddPage(0);
                AddBackground(0, 0, 422, 252, 3500);
                AddImageTiled(10, 36, 403, 4, 2700);
                AddLabel(25, 10, 0, "Dark Forest Chat History - Viewing the last " + Config.HistorySize + " messages");
                AddHtml(20, 40, 395, 195, GenerateHistoryHTML(), false, true);
            }
        }
    }
}