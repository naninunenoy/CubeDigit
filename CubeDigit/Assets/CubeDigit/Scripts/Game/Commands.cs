using VitalRouter;
using VitalRouter.MRuby;

namespace CubeDigit.Game;

[MRubyObject]
public partial struct SetColorCommands : ICommand
{
    public string Id;
    public string Color;
}

[MRubyCommand("set", typeof(SetColorCommands))]
public partial class MyCommandPreset : MRubyCommandPreset { }
