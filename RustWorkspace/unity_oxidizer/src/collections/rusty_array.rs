#[repr(C)]
pub struct RustyArray<T> {
    list: *mut RustyArrayInternal<T>,
}

#[repr(C)]
struct RustyArrayInternal<T> {
    array: *mut T,
    length: usize,
}

impl<T> RustyArray<T> {
    pub fn get_array(&mut self) -> &mut [T] {
        return unsafe {
            std::slice::from_raw_parts_mut((*self.list).array, (*self.list).length)
        };
    }
}