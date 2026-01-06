// Basit WYSIWYG Editör - Resim Upload Özelliği ile

class SimpleEditor {
    constructor(textareaId) {
        this.textarea = document.getElementById(textareaId);
        if (!this.textarea) return;
        
        this.container = document.createElement('div');
        this.container.className = 'simple-editor';
        this.textarea.parentNode.insertBefore(this.container, this.textarea);
        this.textarea.style.display = 'none';
        
        this.createToolbar();
        this.createEditor();
        
        // İlk içeriği yükle
        this.editor.innerHTML = this.textarea.value || '';
        
        // Değişiklikleri textarea'ya aktar
        this.editor.addEventListener('input', () => {
            this.textarea.value = this.editor.innerHTML;
        });
    }
    
    createToolbar() {
        this.toolbar = document.createElement('div');
        this.toolbar.className = 'editor-toolbar';
        this.toolbar.innerHTML = `
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="bold" title="Kalın"><b>B</b></button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="italic" title="İtalik"><i>I</i></button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="underline" title="Altı çizili"><u>U</u></button>
            <span class="mx-2">|</span>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="formatBlock" data-value="h2" title="Başlık 2">H2</button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="formatBlock" data-value="h3" title="Başlık 3">H3</button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="formatBlock" data-value="p" title="Paragraf">P</button>
            <span class="mx-2">|</span>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="insertUnorderedList" title="Liste">•</button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-cmd="insertOrderedList" title="Numaralı Liste">1.</button>
            <span class="mx-2">|</span>
            <button type="button" class="btn btn-sm btn-outline-primary" id="imgBtn" title="Resim Ekle">📷 Resim</button>
            <button type="button" class="btn btn-sm btn-outline-success" id="codeBtn" title="Kod Ekle">&lt;/&gt; Kod</button>
        `;
        this.container.appendChild(this.toolbar);
        
        // Komut butonları
        this.toolbar.querySelectorAll('[data-cmd]').forEach(btn => {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                const cmd = btn.getAttribute('data-cmd');
                const value = btn.getAttribute('data-value');
                document.execCommand(cmd, false, value || null);
                this.editor.focus();
            });
        });
        
        // Resim butonu
        document.getElementById('imgBtn').addEventListener('click', (e) => {
            e.preventDefault();
            this.showImageUpload();
        });
        
        // Kod butonu
        document.getElementById('codeBtn').addEventListener('click', (e) => {
            e.preventDefault();
            this.showCodeModal();
        });
    }
    
    createEditor() {
        this.editor = document.createElement('div');
        this.editor.className = 'editor-area';
        this.editor.contentEditable = 'true';
        this.container.appendChild(this.editor);
    }
    
    showImageUpload() {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        input.onchange = async (e) => {
            const file = e.target.files[0];
            if (file) {
                await this.uploadImage(file);
            }
        };
        input.click();
    }
    
    async uploadImage(file) {
        const formData = new FormData();
        formData.append('file', file);
        
        try {
            const response = await fetch('/admin/upload/image', {
                method: 'POST',
                body: formData
            });
            
            const result = await response.json();
            
            if (result.success) {
                this.insertImage(result.url);
            } else {
                alert('Resim yüklenemedi: ' + result.message);
            }
        } catch (error) {
            alert('Hata: ' + error.message);
        }
    }
    
    insertImage(url) {
        const img = `<img src="${url}" class="img-fluid" alt="Resim" style="max-width: 100%; height: auto;" /><p><br></p>`;
        document.execCommand('insertHTML', false, img);
        this.textarea.value = this.editor.innerHTML;
    }
    
    showCodeModal() {
        // Modal oluştur
        const modalId = 'codeModal' + Date.now();
        const modalHtml = `
            <div class="modal fade" id="${modalId}" tabindex="-1">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Kod Bloğu Ekle</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3">
                                <label class="form-label">Programlama Dili:</label>
                                <select class="form-select" id="langSelect${modalId}">
                                    <option value="javascript">JavaScript</option>
                                    <option value="python">Python</option>
                                    <option value="csharp">C#</option>
                                    <option value="java">Java</option>
                                    <option value="cpp">C++</option>
                                    <option value="go">Go</option>
                                    <option value="rust">Rust</option>
                                    <option value="sql">SQL</option>
                                    <option value="html">HTML</option>
                                    <option value="css">CSS</option>
                                    <option value="php">PHP</option>
                                </select>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Kod:</label>
                                <textarea class="form-control" id="codeArea${modalId}" rows="10" style="font-family: monospace;"></textarea>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">İptal</button>
                            <button type="button" class="btn btn-primary" id="insertBtn${modalId}">Ekle</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
        
        document.body.insertAdjacentHTML('beforeend', modalHtml);
        const modal = new bootstrap.Modal(document.getElementById(modalId));
        modal.show();
        
        // Ekle butonu
        document.getElementById('insertBtn' + modalId).addEventListener('click', () => {
            const code = document.getElementById('codeArea' + modalId).value;
            const lang = document.getElementById('langSelect' + modalId).value;
            
            if (code.trim()) {
                const escaped = this.escapeHtml(code);
                const codeBlock = `<pre><code class="language-${lang}">${escaped}</code></pre><p><br></p>`;
                document.execCommand('insertHTML', false, codeBlock);
                this.textarea.value = this.editor.innerHTML;
                
                // Syntax highlighting
                if (typeof Prism !== 'undefined') {
                    Prism.highlightAllUnder(this.editor);
                }
            }
            
            modal.hide();
            
            // Modal'ı temizle
            setTimeout(() => {
                document.getElementById(modalId).remove();
                document.querySelector('.modal-backdrop')?.remove();
            }, 300);
        });
    }
    
    escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }
}

// Sayfa yüklendiğinde editörleri başlat
document.addEventListener('DOMContentLoaded', function() {
    // ContentTr ve ContentEn alanları için editör başlat
    ['ContentTr', 'ContentEn'].forEach(id => {
        const elem = document.getElementById(id);
        if (elem && elem.tagName === 'TEXTAREA') {
            new SimpleEditor(id);
        }
    });
    
    // Mevcut kod bloklarını highlight et
    if (typeof Prism !== 'undefined') {
        Prism.highlightAll();
    }
});

