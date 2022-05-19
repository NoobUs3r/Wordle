using System;

namespace Wordle
{
    static class NoApiPlay
    {
        public static string GetRandom5LetterWord()
        {
            string[] words5Letters = { "Adult", "Agent", "Anger", "Apple", "Award", "Basis", "Beach", "Birth",
                                       "Chief", "Child", "Claim", "Class", "Clock", "Coach", "Coast", "Dress",
                                       "Drink", "Drive", "Earth", "Enemy", "Entry", "Error", "Event", "Faith",
                                       "Fault", "Field", "Fight", "Final", "Grass", "Green", "Group", "Guide",
                                       "Heart", "Henry", "Horse", "Hotel", "Level", "Lewis", "Light", "Limit",
                                       "Lunch", "Major", "March", "Match", "Metal", "Model", "Night", "Noise",
                                       "North", "Novel", "Nurse", "Offer", "Order", "Other", "Owner", "Panel",
                                       "Paper", "Party", "Peace", "Queen", "Radio", "Range", "Ratio", "Reply",
                                       "Right", "Study", "Stuff", "Style", "Sugar", "Table", "Taste", "Terry",
                                       "Theme", "Thing", "Uncle", "Union", "Unity", "Value", "Video", "Visit",
                                       "Voice", "Waste", "Watch", "Water", "While", "Youth" };
            int rndm = GetRandomNumber(0, words5Letters.Length - 1);
            return words5Letters[rndm];
        }
        
        private static int GetRandomNumber(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to);
        }
    }
}
