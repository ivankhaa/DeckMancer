using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Behaviour
    {
        virtual public void Start() { }
        virtual public void Update() { }
        virtual public void OnMouseDown(GameObject g, MouseButtonEventArgs e) { }
        virtual public void OnMouseUp(GameObject g, MouseButtonEventArgs e) { }
        virtual public void OnMouseMove(GameObject g, MouseMoveEventArgs e) { }
        virtual public void OnMouseWheel(MouseWheelEventArgs e) { }
        virtual public void OnKeyDown(KeyboardKeyEventArgs e) { }
        virtual public void OnKeyUp(KeyboardKeyEventArgs e) { }

    }
}