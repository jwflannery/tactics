using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue/Lines", order = 1)]
public class DialogueLines : ScriptableObject {

    [System.Serializable]
    public class Line : IEnumerable<Line>
    {
        public Line(string lineString, Sprite image)
        {
            this.LineString = lineString;
            this.PortaitImage = image;
        }

        List<Line> myList = new List<Line>();
        public IEnumerator<Line> GetEnumerator()
        {
            return myList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string LineString;
        public Sprite PortaitImage;     
    };

    public Line[] lines =
    {
        new Line("Hi.", null),
        new Line("This is a set of lines.", null),
        new Line("If you can read this...", null),
        new Line("It means the dialogue system is working!", null)
    };
}