
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-gym";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "Gym";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Truong Giang Hoang", "truonggiangh@student.unimelb.edu.au"),
        new TeamMember("Tran Dao Le", "trandaol@student.unimelb.edu.au"),
        new TeamMember("Tuan Phong Vu", "tuanphongv@student.unimelb.edu.au"),
        
    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "Flap Birdy";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"This game is inspired by the original game Flappy Bird. Main objective of the game is to avoid dying by 
slotting in the gaps between pipes to survive. This game can be played in either First or Third Person.
This game will include add-ons for players to collect, such as weapons to destroy the pipes. 3D graphical elements
will be applied in both point-of-views.
";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://www.youtube.com/watch?v=FtjqctPG9hg";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
