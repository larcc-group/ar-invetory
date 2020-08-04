const restful = require('node-restful')
const mongoose = restful.mongoose

const objetoSchema = new mongoose.Schema({
    Nome: { type: String, required: true },
    Descricao: { type: String, required: true },
    AR: { type: Boolean, required: true },
    Campos: { type: Object, required: false },
    Filhos: { type: Array, required: false }
})

objetoSchema.statics.getCampos = async function(name) {
    try{
        let result = await this.findOne({ Nome: name }, { _id: 0, Campos: 1 });
        return result.Campos;
    } catch (ex){
        console.log('ERR', ex)
    }

};

module.exports = restful.model('objetos', objetoSchema)