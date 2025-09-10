using Microsoft.AspNetCore.Identity;

namespace DadJokes.Models
{
    public class Joke
    {
        public int Id { get; set; }

        public int Upvote { get; set; }

        public int Downvote { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string UserId { get; set; } //user who created it


        public Joke()
        {
                
        }
    }
}
