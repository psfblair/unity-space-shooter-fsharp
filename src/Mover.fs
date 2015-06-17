namespace Space_Shooter

open UnityEngine

type Mover() = 
    inherit MonoBehaviour()

    [<SerializeField>] 
    let mutable speedScale: float32 = 1.0f

    member this.rigidbody = this.GetComponent<Rigidbody>()

    member this.Start () =
        this.rigidbody.velocity <- this.transform.forward * speedScale