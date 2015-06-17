namespace Space_Shooter

open UnityEngine

type DestroyByBoundary() =
    inherit MonoBehaviour()

    member this.OnTriggerExit (other: Collider) =
        Object.Destroy(other.gameObject)