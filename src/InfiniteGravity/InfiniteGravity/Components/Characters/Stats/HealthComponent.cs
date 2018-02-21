namespace InfiniteGravity.Components.Characters.Stats {
    public class HealthComponent {
        public float maxHealth;
        public float health;

        public HealthComponent(float maxHealth = 1f) {
            this.maxHealth = maxHealth;
            health = this.maxHealth;
        }

        public float damage {
            get => 1 - (health / maxHealth);
            set => health = (1 - value) * maxHealth;
        }

        public static implicit operator float(HealthComponent healthComponent) {
            return healthComponent.health;
        }
    }
}