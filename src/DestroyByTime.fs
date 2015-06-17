namespace Space_Shooter

open UnityEngine

type DestroyByTime() =
    inherit MonoBehaviour()

    [<SerializeField>] 
    let mutable lifetime: float32 = 1.0f

    member this.Start () =
        Object.Destroy(this.gameObject, lifetime)
