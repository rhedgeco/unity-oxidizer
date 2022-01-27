#[repr(C)]
pub struct RustyList<T> {
    array: *mut T,
    length: i32,
    capacity: usize,
}

pub struct RustyListHandler<'a, T> {
    array: &'a mut [T],
    source: &'a mut RustyList<T>,
}

impl<T> RustyList<T> {
    pub fn get_list_handler(&mut self) -> RustyListHandler<T> {
        return unsafe {
            RustyListHandler {
                array: std::slice::from_raw_parts_mut(
                    self.array, self.capacity),
                source: self,
            }
        };
    }
}

impl<'a, T> RustyListHandler<'a, T> {
    pub fn length(&self) -> i32 { return self.source.length; }

    pub fn capacity(&self) -> usize { return self.source.capacity; }

    pub fn is_full(&self) -> bool { return self.length() == self.capacity() as i32; }

    pub fn clear(&mut self) { self.source.length = 0; }

    pub fn get(&self, index: usize) -> &T { return &self.array[index]; }

    pub fn add(&mut self, item: T) -> bool {
        if self.is_full() { return false; }

        self.array[self.length() as usize] = item;
        self.source.length += 1;
        return true;
    }
}