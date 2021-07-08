namespace SaveSystem.Data
{
    [System.Serializable]
    public class TutorialData
    {
        public bool IntroPlayed;
        public bool FirstInspect;
        public bool FirstClue;


        public TutorialData()
        {
            IntroPlayed = true;
            FirstInspect = true;
            FirstClue = true;
        }
        public TutorialData(bool tutorial)
        {
            IntroPlayed = tutorial;
            FirstInspect = tutorial;
            FirstClue = tutorial;
        }
    }
}
