namespace Space_Shooter

open UnityEngine

type DestroyByContact() =
    inherit MonoBehaviour()

    [<SerializeField>] 
    let mutable scoreValue: int = 1

    [<SerializeField>] [<DefaultValue>] val mutable explosion: GameObject
    [<SerializeField>] [<DefaultValue>] val mutable playerExplosion: GameObject
    [<SerializeField>] [<DefaultValue>] val mutable private gameController: GameController

    member this.Start () =
        let gameControllerComponent = GameObject.FindWithTag("GameController")
        try
            this.gameController <- gameControllerComponent.GetComponent<GameController>()
        with
            | problem -> Debug.Log("Cannot find GameController script.")

    member this.OnTriggerEnter (other: Collider) = 
        if other.tag <> "Boundary"
        then 
            Object.Destroy(other.gameObject)
            Object.Destroy(this.gameObject)

            if other.tag = "Player" then
                Object.Instantiate(this.playerExplosion, other.transform.position, other.transform.rotation) |> ignore
                this.gameController.GameOver()
            else                 
                Object.Instantiate(this.explosion, this.transform.position, this.transform.rotation) |> ignore
                this.gameController.AddScore(scoreValue)

