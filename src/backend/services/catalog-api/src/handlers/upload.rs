use std::io;
use rocket::data::ToByteUnit;
use rocket::http::uri::Absolute;
use rocket::response::content::RawText;
use rocket::tokio::fs::File;
use rocket::{Data};

use crate::paste_id::PasteId;


// In a real application, these would be retrieved dynamically from a config.
const HOST: Absolute<'static> = uri!("http://localhost:8000");
const ID_LENGTH: usize = 3;

#[post("/upload", data = "<paste>")]
pub async fn upload(paste: Data<'_>) -> io::Result<String> {
    let id = PasteId::new(ID_LENGTH);
    paste
        .open(128.kilobytes())
        .into_file(id.file_path())
        .await?;
    Ok(uri!(HOST, retrieve(id)).to_string())
}

#[get("/retrieve/<id>")]
pub async fn retrieve(id: PasteId<'_>) -> Option<RawText<File>> {
    File::open(id.file_path()).await.map(RawText).ok()
}