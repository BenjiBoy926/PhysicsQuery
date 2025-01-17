using System.Linq;

namespace PhysicsQuery.Editor
{
    public class Preview
    {
        public static string[] Labels => _previews.Select(x => x.Label).ToArray();
        public static int Count => _previews.Length;
        public string Label { get; private set; }
        private GizmoMode Mode { get; set; }
        private ResultDisplay Display { get; set; }

        private static readonly Preview[] _previews = new Preview[]
        {
            new("Cast", new GizmoMode_Cast(), new ResultDisplay_Cast()),
            new("Overlap", new GizmoMode_Overlap(), new ResultDisplay_Overlap()),
        };

        private Preview(string label, GizmoMode mode, ResultDisplay display)
        {
            Label = label;
            Mode = mode;
            Display = display;
        }
    
        public static Preview Get(int i)
        {
            return _previews[i];
        }
        public void SetGizmoModeOn(PhysicsQuery query)
        {
            query.SetGizmoMode(Mode);
        }
        public void DrawResultDisplayInspectorGUI()
        {
            Display.DrawInspectorGUI(Mode);
        }
    }
}