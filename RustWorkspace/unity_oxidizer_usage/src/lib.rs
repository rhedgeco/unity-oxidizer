use unity_oxidizer::collections::rusty_array::RustyArray;
use unity_oxidizer::collections::rusty_list::RustyList;

#[no_mangle]
pub extern "C" fn test_array_populate(rusty_array: &mut RustyArray<f32>) {
    let array = rusty_array.get_array();

    for i in 0..array.len() {
        array[i] = i as f32;
    }
}

#[no_mangle]
pub extern "C" fn test_list_populate(rusty_array: &mut RustyList<f32>) {
    let mut list = rusty_array.get_list_handler();
    list.clear();

    for i in 0..list.length() {
        list.add(i as f32);
    }
}