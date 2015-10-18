using UnityEngine;
using System.Collections;

public abstract class BaseTower : MonoBehaviour {
	protected float bulletSpeed, fireSpeed, turnDamp;
	protected int bulletDamage;
	protected string towerName;
	protected int destroyReward;

	private float fireTick;

	private float aggroRadius;

	public GameObject bullet;
	private GameObject bulletSpawnObj;
	private ArrayList potentialTargets;
	private Transform target;
	private bool hasTarget;

	[HideInInspector]
	public bool activated;

	private InputHandler inputHandler;

	// TODO: Could have a public virtual method to display animations on firing, etc.
	// where specific towers would have to override...

	public BaseTower(float bulletSpeed, float fireSpeed, int bulletDamage, string towerName, float turnDamp, int destroyReward){
		this.bulletSpeed = bulletSpeed;
		this.fireSpeed = fireSpeed;
		this.bulletDamage = bulletDamage;
		this.towerName = towerName;
		this.turnDamp = turnDamp;
		this.destroyReward = destroyReward;
	}

	// Use this for initialization
	protected void Start () {
		inputHandler = GameObject.Find ("Main Object").GetComponent<InputHandler> ();

		showHideSelector (false);

		activated = false;
		aggroRadius = gameObject.collider2D.GetComponent<CircleCollider2D>().radius;
		hasTarget = false;
		fireTick = 0;
		potentialTargets = new ArrayList ();

		Transform[] ts = GetComponentsInChildren<Transform> ();

		foreach(Transform t in ts){
			if(t.tag == "BulletSpawn"){
				bulletSpawnObj = t.gameObject;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.curGameState == GameState.Paused)
			return;

		if(!activated) return; // Dragging or something

		if (fireTick < fireSpeed) {
			fireTick++;		
		}

		getTarget ();

		if (hasTarget) {
			handleTowerRotation();

			if(fireTick >= fireSpeed){
				fireRound();
			}
		}
	}

	void fireRound(){
		GameObject b = Instantiate (bullet, bulletSpawnObj.transform.position, Quaternion.identity) as GameObject;
		b.GetComponent<Bullet> ().setDamage (bulletDamage);
		Vector2 forceVec = transform.up * bulletSpeed;
		b.GetComponent<Bullet> ().forceVec = forceVec;
		b.rigidbody2D.AddForce (forceVec);	
		fireTick = 0;
	}

	void getTarget(){
		// if we have a target and it's still in range then keep that target and return
		if (target && hasTarget && inRange(target)) return;
		hasTarget = false;

		if (potentialTargets == null) {
			return;
		}

		for (int i = 0; i < potentialTargets.Count; i++){
			if(!(Transform)potentialTargets[i]){
				potentialTargets.RemoveAt(i);
			}
			else if(inRange((Transform)potentialTargets[i])){
				target = (Transform)potentialTargets[i];
				hasTarget = true;
			}

			if(hasTarget) return;
		}
	}

	bool inRange(Transform t){
		float distance = Vector3.Distance(transform.position, t.position);
		if (distance <= aggroRadius) {
			return true;		
		}

		return false;
	}

	public void addTarget(Transform t){
		if(potentialTargets == null)
			Debug.Log("potential null");
		if(t == null)
			Debug.Log("t null");

		if(!potentialTargets.Contains(t))
			potentialTargets.Add (t);
	}

	void handleTowerRotation(){
		Vector3 mainPos = transform.position;
		float diffX = target.transform.position.x - mainPos.x;
		float diffY = target.transform.position.y - mainPos.y;
		float angle = (Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg) - 90;
		Vector3 rotVec = new Vector3 (0, 0, angle);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (rotVec), Time.deltaTime * turnDamp);
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.tag == "Enemy"){
			addTarget (c.transform);
		}
	}

	public void clicked(){
		inputHandler.towerClicked (this);
		showHideSelector (true);
	}

	public void showHideSelector(bool val){
		foreach (Transform t in transform){
			if(t.name == "Selector"){
				foreach(Transform child in t){
					child.renderer.enabled = val;
				}
			}
		}
	}

	public void destroy(){
		Destroy (gameObject);
	}

	public int getDestroyReward(){
		return destroyReward;
	}

	public string getTowerName(){
		return towerName;
	}
}