const restful = require('node-restful')
const mongoose = restful.mongoose

const componenteSchema = new mongoose.Schema({
    Nome: { type: String, required: true },
    Tipo: { type: String, required: true },
    Modelo: { type: String, required: false },
    Fabricante: { type: String, required: false },
    Funcao: { type: String, required: false },
    Marcador: { type: String, required: false },
    Projecao: { type: Number, required: false },
    Detalhes: { type: Array, required: false },
    Filhos: { type: Array, required: false },
    Logs: { type: Array, required: false },
})

componenteSchema.statics.getByMarcador = async function(macador) {
    try{
        let result = await this.findOne({ Marcador: macador });
        return result;
    } catch (ex){
        console.log('ERR', ex)
    }

};



module.exports = restful.model('componentes', componenteSchema)