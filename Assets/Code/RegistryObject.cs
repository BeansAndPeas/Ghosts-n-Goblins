using System;

public class RegistryObject<T> {
        public readonly string name;
        private readonly Func<T> @object;

        public RegistryObject(string name, Func<T> tile) {
            this.name = name;
            this.@object = tile;
        }

        public T Get() {
            return this.@object.Invoke();
        }
    }