## Unity Space Shooter - F\# 

This is a version of the Unity Space Shooter beginner tutorial
with slight modifications for Unity 5 (primarily, using UI text
rather than GUI text).

Scripts are written in the F# programming language. This provides
some examples of how to expose fields in subclasses of MonoBehaviour
so that Unity can inject dependencies (and so that they show up
in the Unity inspector). The GameController script also includes
an example of how to create a coroutine in F#.

Some other things I learned along the way:

#### Library references:

To make things work with .NET 3.5, you can't use F# 3.1. The easiest way to get off
the ground is to remove FSharp.Core from your project references and then add the NuGet
package FSharp.Core for F# 3.0, which contains an FSharp.Core that will work with .NET 3.5.

For Mac users, here are the locations of the Unity libraries to add to your project
references:

/Applications/Unity/Unity.app/Contents/Frameworks/Mono/lib/mono/unity/mscorlib.dll
/Applications/Unity/Unity.app/Contents/Frameworks/Managed/UnityEngine.dll
/Applications/Unity/Unity.app/Contents/UnityExtensions/Unity/GUISystem/UnityEngineUI.dll

### Links

* [Unity] (http://unity3d.com)
* [How to use F# libraries with Unity] (https://github.com/eriksvedang/FSharp-Unity)
* [Unity tutorial: Roll-a-ball F-Sharp] (https://github.com/Thorium/Roll-a-ball-FSharp)
