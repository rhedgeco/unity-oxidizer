# Rusty Collections
There are 2 collection object styles that are maintained here.

### 1. Internal Representations
The internal representation contain all the data that is important to rust.

These will be created in unmanaged memory, and contain data that links it back to the Unity native representations.

They are declared as internal so that they will never be used outside of this library, and cannot be created in a managed context.
They need to be pinned in unmanaged memory so that pointers to them remain valid, and their content stays where it should be.

### 2. Rusty Representations
These are simply a wrapper for the internal representations.

They have a generic type description, but this is solely to maintain some kind of 'loose' type safety while passing the objects around.
The generic type is used nowhere in the struct, and simply helps the user of the object know what kind of data it contains.