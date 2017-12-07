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
		aggroRadius = gameObject.GetComponent<Collider2D>().GetComponent<CircleCollider2D>().radius;
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
		if (GameManager.curGameState != GameState.Paused && GameManager.curGameState != GameState.GameOver) {
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
	}

	void fireRound(){
		GameObject b = Instantiate (bullet, bulletSpawnObj.transform.position, Quaternion.identity) as GameObject;
		Bullet newBullet = b.GetComponent<Bullet> ();
		newBullet.setDamage (bulletDamage);
		newBullet.setMaxDistance (aggroRadius);
		Vector2 forceVec = transform.up * bulletSpeed;
		newBullet.forceVec = forceVec;
		b.GetComponent<Rigidbody2D>().AddForce (forceVec);	
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

	/**
	 * Called when the user selects the tower - lets the input handler know and shows this
	 * tower's indicators.
	 */
	public void selected(){
		inputHandler.towerClicked (this);
		showHideSelector (true);
		showHideAggroRadius (true);
	}

	/**
	 * Hides this tower's indicators.
	 */
	public void deSelected(){
		showHideSelector (false);
		showHideAggroRadius (false);
	}

	/**
	 * Can show or hide this tower's selector indicator.
	 */
	private void showHideSelector(bool val){
		foreach (Transform t in transform){
			if(t.name == "Selector"){
				foreach(Transform child in t)
					child.GetComponent<Renderer>().enabled = val;
			}
		}
	}

	/**
	 * Can show or hide this tower's aggro radius indicator.
	 */
	private void showHideAggroRadius(bool val){
		foreach (Transform t in transform){
			if(t.name == "AggroRadius")
				t.GetComponent<Renderer>().enabled = val;
		}
	}

	/**
	 * Removes this tower from everything.
	 */
	public void destroy(){
		Destroy (gameObject);
	}

	/**
	 * Returns the amount that this tower is worth destroying for.
	 */
	public int getDestroyReward(){
		return destroyReward;
	}

	/**
	 * Returns the name of this tower.
	 */
	public string getTowerName(){
		return towerName;
	}

	public float getBulletSpeed() {
		return bulletSpeed;
	}

	public float getFireSpeed() {
		return fireSpeed;
	}

	public int getBulletDamage() {
		return bulletDamage;
	}

	public float getTurnDamp() {
		return turnDamp;
	}

	public void increaseBulletSpeed() {
		bulletSpeed += 5;
	}

	public void increaseTurnSpeed() {
		turnDamp += 0.25f;
	}

	public void increaseBulletDamage() {
		bulletDamage += 1;
	}

	public void decreaseFireDelay() {
		fireSpeed -= 0.25f;
	}
}