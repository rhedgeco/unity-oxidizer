#[repr(C)]
pub struct RustyList<'a, T> {
    list: &'a mut RustyListInternal<T>,
}

#[repr(C)]
struct RustyListInternal<T> {
    array: *mut T,
    length: i32,
    capacity: usize,
}

pub struct RustyListHandler<'a, T> {
    array: &'a mut [T],
    source: &'a mut RustyListInternal<T>,
}

impl<'a, T> RustyList<'a, T> {
    pub fn get_list_handler(&mut self) -> RustyListHandler<T> {
        return unsafe {
            RustyListHandler {
                array: std::slice::from_raw_parts_mut(self.list.array, self.list.capacity),
                source: self.list,
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

        self.array[self.source.length as usize] = item;
        self.source.length += 1;
        return true;
    }
}