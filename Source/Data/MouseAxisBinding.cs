namespace Celeste64;

public class MouseAxisBinding : VirtualButton.IBinding
{
    public Vec2 Axis;

    public bool IsPressed => false;
    public bool IsDown { get; private set; }
    public bool IsReleased => false;
    public float Value => ValueNoDeadzone;

    public float ValueNoDeadzone
    {
        get
        {
            var movement = Input.Mouse.Position - Input.LastState.Mouse.Position;
            var dot = Vec2.Dot(movement, Axis) / ((11-Save.Instance.MouseSensitivity) * 4);
            IsDown = dot > 0;
            if (IsDown)
            {
                if (Axis.Y != 0)
                    // less Y sensibility feel better
                    dot = Calc.Clamp(dot, 0.4f, 1.5f);
                else
                    dot = Calc.Clamp(dot, 0.4f, 2);
            }
            return dot;
        }
    }

    public VirtualButton.ConditionFn? Enabled { get; set; }
}