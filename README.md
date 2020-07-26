# cheat-detection-osu
 
Process analyzer for osu!. Best to be used by server owners while requesting a liveplay.

# Usage:

Before telling a play to run it, change the login details in Program.cs for your sftp server.
Tell the suspected player to run it while playing osu!
The software will collect logs and upload them to your desired sftp server every 30 seconds.
After the play is done, you download the logs to your computer, analyze them, and make a decision about the player.
