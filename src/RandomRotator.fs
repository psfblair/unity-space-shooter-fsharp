namespace Space_Shooter

open UnityEngine

type RandomRotator() =
    inherit MonoBehaviour()

    [<SerializeField>] 
    let mutable tumble: float32 = 1.0f

    member this.rigidbody = this.GetComponent<Rigidbody>()

    member this.Start () =
        this.rigidbody.angularVelocity <- Random.insideUnitSphere * tumble