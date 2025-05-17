using VitalRouter;
using VitalRouter.MRuby;

namespace CubeDigit.Game;

[MRubyObject]
public partial struct InitCubesCommand : ICommand
{
    public int X;
    public int Y;
    public int Z;
    public float Size;
    public float Spacing;
}

[MRubyObject]
public partial struct SetColorCommand : ICommand
{
    public string Id;
    public string Color;
}

[MRubyCommand("init", typeof(InitCubesCommand))]
[MRubyCommand("set", typeof(SetColorCommand))]
public partial class MyCommandPreset : MRubyCommandPreset { }
