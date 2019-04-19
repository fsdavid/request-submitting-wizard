export class NoteModel {
  noteId: number;
  requestId: number;
  noteType: string;
  content: string;
  noteFiles: string[];
  constructor ( noteId: number,
                requestId: number,
                noteType: string,
                content: string,
                noteFiles: string[]) {
    this.noteId = noteId;
    this.requestId = requestId;
    this.noteType = noteType;
    this.content = content;
    this.noteFiles = noteFiles;
  }
}
